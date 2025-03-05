import * as React from "react";
import FormComponent from "../components/FormComponent";
import { useNavigate } from "react-router";
import { SubmitLogin } from "../utils";
import { useFormContext } from "../contexts/FormContext";

const LoginPage: React.FC = () => {
  const { formData } = useFormContext();
  const [errors, setErrors] = React.useState<Record<string, string | null>>({
    email: null,
    password: null,
    submitError: null,
  });
  const navigate = useNavigate();

  React.useEffect(() => {
    console.log(errors);
  }, [errors]);

  return (
    <div className="page-container">
      <div className="form-header">Welcome to Y-Chat</div>
      <div className="form-subheader">Live chat built by Yehor Katkov</div>
      <hr className="form-linebreak" />
      <div className="form-container">
        <FormComponent
          submitButtonText="Continue"
          submitButtonOnClick={async () =>
            await SubmitLogin(formData, navigate, setErrors)
          }
        >
          <label htmlFor="email" className="input-label">
            Email
          </label>
          <input
            type="text"
            name="email"
            id="email"
            className="input"
            placeholder="Type your email"
          />
          {errors.email && <div className="error-message">{errors.email}</div>}
          <label htmlFor="password" className="input-label">
            Password
          </label>
          <input
            type="password"
            name="password"
            id="password"
            className="input"
            placeholder="Type your password"
          />
          {errors.password && (
            <div className="error-message">{errors.password}</div>
          )}
        </FormComponent>
        <div className="useful-links">
          <div
            className="useful-link"
            onClick={() => navigate("/registration")}
          >
            Sign up
          </div>
          <div className="useful-link">Forgot password?</div>
        </div>
        {errors.submitError && (
          <div className="error-message">{errors.submitError}</div>
        )}
      </div>
    </div>
  );
};

export default LoginPage;
