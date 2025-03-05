const url = "http://localhost:5226";

export const SubmitLogin = async (
  formData: Record<string, any>,
  navigate: (path: string) => void,
  setErrors: (errors: Record<string, string | null>) => void
) => {
  const errors = ValidateLoginForm(formData);

  setErrors(errors);

  if (!Object.values(errors).some((e) => e === null)) {
    return;
  }

  const response = await fetch(`${url}/api/auth/login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(formData),
  });

  const data = await response.json();

  if (response.status !== 200) {
    errors.submitError = data.message;
    setErrors(errors);
    return;
  }

  localStorage.setItem("token", data.jwtToken.token);

  navigate("/");
};

export const SubmitRegistration = async (
  formData: Record<string, any>,
  navigate: (path: string) => void,
  setErrors: (errors: Record<string, any>) => void
) => {
  const errors = ValidateRegistrationForm(formData);
  console.log(errors);
  setErrors(errors);

  if (!Object.values(errors).some((e) => e === null)) {
    return;
  }

  const response = await fetch(`${url}/api/auth/register`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      email: formData.email,
      password: formData.password,
      captcha: formData.googleCheck,
    }),
  });

  const data = await response.json();

  if (response.status !== 200) {
    errors.submitError = data.message;
    setErrors(errors);
    return;
  }

  localStorage.setItem("token", data.jwtToken.token);

  navigate("/");
};

export const SubmitProfileInformation = async (
  formData: Record<string, any>,
  navigate: (path: string) => void,
  setErrors: (errors: Record<string, any>) => void
) => {
  const errors = ValidateProfileInformationForm(formData);
  console.log(errors);
  setErrors(errors);

  if (!Object.values(errors).some((e) => e === null)) {
    return;
  }

  const response = await fetch(`${url}/api/auth/add-profile-details`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${localStorage.getItem("token")}`,
    },
    body: JSON.stringify({
      image: formData.image,
      username: formData.username,
      birthDate: formData["birth-date"],
    }),
  });

  const data = await response.json();

  if (response.status !== 200) {
    errors.submitError = data.message;
    setErrors(errors);
    return;
  }

  navigate("/");
};

export const FetchIndexData = async () => {
  const response = await fetch(`${url}/api/test`, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${localStorage.getItem("token")}`,
    },
  });

  const data = await response.json();

  if (response.status === 401) {
    throw new Error(data.message);
  }

  return data;
};

const ValidateLoginForm = (formData: Record<string, any>) => {
  const errors: Record<string, string | null> = {
    email: null,
    password: null,
    submitError: null,
  };

  if (!formData.email || !formData.password) {
    errors.submitError = "Please fill in all fields";
  }

  const emailRegex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

  if (!emailRegex.test(formData.email)) {
    errors.email = "Invalid email";
  }
  const passwordRegex = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/;

  if (!passwordRegex.test(formData.password)) {
    errors.password =
      "Password must contain at least 8 characters, one number and letter";
  }

  return errors;
};

const ValidateRegistrationForm = (formData: Record<string, any>) => {
  const errors = ValidateLoginForm(formData);
  errors["confirm-password"] = null;
  errors.googleCheck = null;

  if (
    !formData.email ||
    !formData.password ||
    !formData["confirm-password"] ||
    !formData.googleCheck
  ) {
    errors.submitError = "Please fill in all fields";
  }

  if (formData.password !== formData["confirm-password"]) {
    errors.confirmPassword = "Passwords do not match";
  }

  if (!formData.googleCheck) {
    errors.googleCheck = "Please confirm you are not a robot";
  }

  return errors;
};

const ValidateProfileInformationForm = (formData: Record<string, any>) => {
  const errors: Record<string, string | null> = {
    image: null,
    username: null,
    "birth-date": null,
    submitError: null,
  };

  if (!formData.image || !formData.username || !formData["birth-date"]) {
    errors.submitError = "Please fill in all fields";
  }

  if (!formData.image) {
    errors.image = "Please upload an image";
  }

  if (!formData.username) {
    errors.username = "Please fill in your username";
  }

  if (!formData["birth-date"]) {
    errors["birth-date"] = "Please fill in your birth date";
  }

  return errors;
};
