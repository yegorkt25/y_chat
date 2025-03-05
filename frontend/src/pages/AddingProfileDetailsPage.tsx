import * as React from "react";
import FormComponent from "../components/FormComponent";
import { useFormContext } from "../contexts/FormContext";
import { useNavigate } from "react-router";
import { SubmitProfileInformation } from "../utils";
import AvatarUploader from "../components/AvatarUploader";

const AddingProfileDetailsPage: React.FC = () => {
  const { formData } = useFormContext();
  const navigate = useNavigate();
  const [errors, setErrors] = React.useState<Record<string, string | null>>({
    image: null,
    username: null,
    "birth-date": null,
    submitError: null,
  });
  const fileInputRef = React.useRef<HTMLInputElement | null>(null);

  return (
    <div className="page-container">
      <div className="form-header">Fill in personal information</div>
      <hr className="form-linebreak additionalMargin" />
      <div className="form-container">
        <FormComponent
          submitButtonText="Add profile details"
          submitButtonOnClick={() =>
            SubmitProfileInformation(formData, navigate, setErrors)
          }
        >
          <label className="input-label">Avatar</label>

          <AvatarUploader />
          {errors.image && <div className="error-message">{errors.image}</div>}

          <input
            ref={fileInputRef}
            type="file"
            accept="image/png, image/jpeg"
            name="image"
            id="image"
            className="input hidden"
          />

          <label htmlFor="username" className="input-label">
            Username
          </label>

          <input
            type="text"
            name="username"
            id="username"
            className="input"
            placeholder="Type your username"
          />

          {errors.username && (
            <div className="error-message">{errors.username}</div>
          )}

          <label htmlFor="birth-date" className="input-label">
            Birth date
          </label>
          <input
            type="date"
            name="birth-date"
            id="birth-date"
            className="input"
            placeholder="Confirm your password"
          />
          {errors["birth-date"] && (
            <div className="error-message">{errors["birth-date"]}</div>
          )}
        </FormComponent>
        <div className="useful-links">
          <div className="useful-link">Back to login</div>
        </div>
        {errors.submitError && (
          <div className="error-message">{errors.submitError}</div>
        )}
      </div>
    </div>
  );
};

export default AddingProfileDetailsPage;
