import { useState } from "react";
import "./App.css";
import User from "./Types/User";
import LogoLayout from "./layouts/LogoLayout";
import LoginPage from "./pages/LoginPage";
import RegistrationPage from "./pages/RegistrationPage";
import ConfirmEmailPage from "./pages/ConfirmEmailPage";
import AddingProfileDetailsPage from "./pages/AddingProfileDetailsPage";
import { FormProvider } from "./contexts/FormContext";

function App() {
  const [user, setUser] = useState<User | null>(null);

  const component =
    user === null ? <AddingProfileDetailsPage /> : <>You've been authorized</>;

  return (
    <>
      <FormProvider>
        <LogoLayout>{component}</LogoLayout>
      </FormProvider>
    </>
  );
}

export default App;
