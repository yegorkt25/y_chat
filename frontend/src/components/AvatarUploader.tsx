import React, { useState, useCallback, useRef } from "react";
import Cropper from "react-easy-crop";
import Dropzone from "react-dropzone";
import { useFormContext } from "../contexts/FormContext";

const AvatarUploader: React.FC = () => {
  const [image, setImage] = useState<string | null>(null);
  const [crop, setCrop] = useState({ x: 0, y: 0 });
  const [zoom, setZoom] = useState(1);
  const { formData, setFormData } = useFormContext();

  const cropAreaPixelsRef = useRef<{
    x: number;
    y: number;
    width: number;
    height: number;
  } | null>(null);

  const handleDrop = (acceptedFiles: File[]) => {
    const file = acceptedFiles[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => setImage(reader.result as string);
      reader.readAsDataURL(file);
    }
  };

  const onCropComplete = useCallback((_: any, croppedAreaPixels: any) => {
    cropAreaPixelsRef.current = croppedAreaPixels;
  }, []);

  const getCroppedImage = async () => {
    if (!image || !cropAreaPixelsRef.current) return;

    const img = new Image();
    img.src = image;
    await img.decode();

    const canvas = document.createElement("canvas");
    const ctx = canvas.getContext("2d");
    if (!ctx) return;

    const { x, y, width, height } = cropAreaPixelsRef.current;
    canvas.width = width;
    canvas.height = height;
    ctx.drawImage(img, x, y, width, height, 0, 0, width, height);

    const croppedImage = canvas.toDataURL();
    setFormData({ ...formData, image: croppedImage });
    setImage(null);
  };

  return (
    <div style={{ textAlign: "center", padding: "20px" }}>
      <Dropzone onDrop={handleDrop} accept={{ "image/*": [] }} multiple={false}>
        {({ getRootProps, getInputProps }) => (
          <div
            {...getRootProps()}
            style={{
              border: "2px dashed gray",
              padding: "20px",
              cursor: "pointer",
            }}
          >
            <input {...getInputProps()} />
            <p>{image ? "Change Image" : "Drag & Drop or Click to Upload"}</p>
          </div>
        )}
      </Dropzone>

      {image && (
        <div
          style={{
            position: "relative",
            width: "300px",
            height: "300px",
            margin: "20px auto",
          }}
        >
          <Cropper
            image={image}
            crop={crop}
            zoom={zoom}
            aspect={1}
            onCropChange={setCrop}
            onZoomChange={setZoom}
            onCropComplete={onCropComplete}
          />
        </div>
      )}

      {image && (
        <div>
          <div
            onClick={getCroppedImage}
            className="submit-button secondary-button"
          >
            Save Avatar
          </div>
        </div>
      )}
    </div>
  );
};

export default AvatarUploader;
