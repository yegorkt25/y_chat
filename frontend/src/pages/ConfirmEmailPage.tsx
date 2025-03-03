import * as React from "react";

const ConfirmEmailPage: React.FC = () => {
  return (
    <div className="page-container">
      <div className="form-header">Please, confirm your email to continue</div>
      <div className="useful-links">
        <div className="useful-link">Resend email</div>
      </div>
    </div>
  );
};

export default ConfirmEmailPage;
