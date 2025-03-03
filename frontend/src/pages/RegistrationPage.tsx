import * as React from "react";
import ReCAPTCHA from "react-google-recaptcha";
import FormComponent from "../components/FormComponent";

const RegistrationPage: React.FC = () => {
  return (
    <div className="page-container">
      <div className="form-header">Sign up to Y-Chat</div>
      <div className="form-subheader">Live chat built by Yehor Katkov</div>
      <hr className="form-linebreak" />
      <div className="form-container">
        <FormComponent submitButtonText="Sign up">
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
          <ReCAPTCHA sitekey="6Le9GNsqAAAAAAV_5oGpC9WWAtCin9EJxU6GXoii"></ReCAPTCHA>
        </FormComponent>
        <div className="useful-links">
          <div className="useful-link">Back to login</div>
        </div>
      </div>
    </div>
  );
};

export default RegistrationPage;
