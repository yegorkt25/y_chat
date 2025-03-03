import * as React from "react";
import FormComponent from "../components/FormComponent";

const LoginPage: React.FC = () => {
  return (
    <div className="page-container">
      <div className="form-header">Welcome to Y-Chat</div>
      <div className="form-subheader">Live chat built by Yehor Katkov</div>
      <hr className="form-linebreak" />
      <div className="form-container">
        <FormComponent submitButtonText="Continue">
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
        </FormComponent>
        <div className="useful-links">
          <div className="useful-link">Sign up</div>
          <div className="useful-link">Forgot password?</div>
        </div>
      </div>
    </div>
  );
};

export default LoginPage;
