import * as React from "react";
import { useFormContext } from "../contexts/FormContext";

interface FormComponentProps {
  children: React.ReactNode;
  submitButtonText: string;
}

const FormComponent: React.FC<FormComponentProps> = ({
  children,
  submitButtonText,
}) => {
  const formRef = React.useRef<HTMLFormElement | null>(null);
  const { formData, setFormData } = useFormContext();

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, type, files, value } = e.target;

    if (type === "file" && files) {
      if (files[0]) {
        setFormData({ ...formData, [name]: files[0] });
      }
    } else {
      setFormData({ ...formData, [name]: value });
    }

    console.log(formData);
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    console.log(formData);
  };

  const childrenArray = React.Children.map(children, (child) => {
    if (React.isValidElement(child) && child.type === "input") {
      return React.cloneElement(
        child as React.ReactElement<HTMLInputElement>,
        {
          onChange: handleChange,
        } as Partial<HTMLInputElement>
      );
    }
    return child;
  });

  return (
    <form ref={formRef} onSubmit={handleSubmit} className="form">
      {childrenArray}

      <div
        onClick={() => formRef.current?.requestSubmit()}
        className="submit-button"
      >
        {submitButtonText}
      </div>
    </form>
  );
};

export default FormComponent;
