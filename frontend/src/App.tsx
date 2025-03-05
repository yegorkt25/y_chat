import { useEffect } from "react";
import "./App.css";
import LogoLayout from "./layouts/LogoLayout";
import AddingProfileDetailsPage from "./pages/AddingProfileDetailsPage";
import { FormProvider } from "./contexts/FormContext";
import { BrowserRouter, Route, Routes, useNavigate } from "react-router";
import IndexPage from "./pages/IndexPage";
import LoginPage from "./pages/LoginPage";
import RegistrationPage from "./pages/RegistrationPage";
import ConfirmEmailPage from "./pages/ConfirmEmailPage";

function App() {
  const navigate = useNavigate();

  useEffect(() => {
    const jwt = localStorage.getItem("token");
    if (jwt === null) {
      navigate("/login");
    }
  }, []);

  return (
    <>
      <LogoLayout>
        <FormProvider>
          <Routes>
            <Route path="/" element={<IndexPage />} />
            <Route path="/login" element={<LoginPage />} />
            <Route path="/registration" element={<RegistrationPage />} />
            <Route path="/confirm-email" element={<ConfirmEmailPage />} />
            <Route
              path="/add-profile-details"
              element={<AddingProfileDetailsPage />}
            />
          </Routes>
        </FormProvider>
      </LogoLayout>
    </>
  );
}

const AppWrapper = () => {
  return (
    <BrowserRouter>
      <App />
    </BrowserRouter>
  );
};

export default AppWrapper;
