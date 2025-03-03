import * as React from "react";
import FormComponent from "../components/FormComponent";
import { useFormContext } from "../contexts/FormContext";

const AddingProfileDetailsPage: React.FC = () => {
  const { formData } = useFormContext();
  const fileInputRef = React.useRef<HTMLInputElement | null>(null);

  const handleAvatarChange = () => {
    fileInputRef.current?.click();
  };

  return (
    <div className="page-container">
      <div className="form-header">Fill in personal information</div>
      <hr className="form-linebreak additionalMargin" />
      <div className="form-container">
        <FormComponent submitButtonText="Add profile details">
          <label className="input-label">Avatar</label>

          <div className="file-selection-container">
            <div className="sub-label">
              Choose a file: {formData["image"] && formData["image"].name}
            </div>

            <div
              className="submit-button secondary-button"
              onClick={handleAvatarChange}
            >
              Choose
            </div>
          </div>

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
        </FormComponent>
        <div className="useful-links">
          <div className="useful-link">Back to login</div>
        </div>
      </div>
    </div>
  );
};

export default AddingProfileDetailsPage;
