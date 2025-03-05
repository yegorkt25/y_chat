import * as React from "react";
import { useFormContext } from "../contexts/FormContext";

interface FormComponentProps {
  children: React.ReactNode;
  submitButtonText: string;
  submitButtonOnClick: (navigate?: (path: string) => void) => void;
}

const FormComponent: React.FC<FormComponentProps> = ({
  children,
  submitButtonText,
  submitButtonOnClick,
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
    <form
      ref={formRef}
      onSubmit={(e: any) => {
        e.preventDefault();
        submitButtonOnClick(e);
      }}
      className="form"
    >
      {childrenArray}

      <div
        onClick={() => {
          formRef.current?.requestSubmit();
          submitButtonOnClick();
        }}
        className="submit-button"
      >
        {submitButtonText}
      </div>
    </form>
  );
};

export default FormComponent;
