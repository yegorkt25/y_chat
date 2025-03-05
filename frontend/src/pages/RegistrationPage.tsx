import * as React from "react";
import ReCAPTCHA from "react-google-recaptcha";
import FormComponent from "../components/FormComponent";
import { useNavigate } from "react-router";
import { SubmitRegistration } from "../utils";
import { useFormContext } from "../contexts/FormContext";

const RegistrationPage: React.FC = () => {
  const { formData, setFormData } = useFormContext();
  const navigate = useNavigate();
  const [errors, setErrors] = React.useState<Record<string, any>>({
    email: null,
    password: null,
    confirmPassword: null,
    googleCheck: null,
    submitError: null,
  });

  return (
    <div className="page-container">
      <div className="form-header">Sign up to Y-Chat</div>
      <div className="form-subheader">Live chat built by Yehor Katkov</div>
      <hr className="form-linebreak" />
      <div className="form-container">
        <FormComponent
          submitButtonText="Sign up"
          submitButtonOnClick={async () =>
            await SubmitRegistration(formData, navigate, setErrors)
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
          <label htmlFor="confirm-password" className="input-label">
            Confirm password
          </label>
          <input
            type="password"
            name="confirm-password"
            id="confirm-password"
            className="input"
            placeholder="Confirm your password"
          />
          {errors["confirm-password"] && (
            <div className="error-message">{errors.confirmPassword}</div>
          )}
          <ReCAPTCHA
            sitekey="6Le9GNsqAAAAAAV_5oGpC9WWAtCin9EJxU6GXoii"
            onChange={(val: any) =>
              setFormData({ ...formData, googleCheck: val })
            }
          ></ReCAPTCHA>
          {errors.googleCheck && (
            <div className="error-message">{errors.googleCheck}</div>
          )}
        </FormComponent>
        <div className="useful-links">
          <div className="useful-link" onClick={() => navigate("/login")}>
            Back to login
          </div>
        </div>
        {errors.submitError && (
          <div className="error-message">{errors.submitError}</div>
        )}
      </div>
    </div>
  );
};

export default RegistrationPage;
