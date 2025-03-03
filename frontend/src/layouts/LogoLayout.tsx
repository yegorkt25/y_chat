import * as React from "react";

interface ILogoLayoutProps {
  children: React.ReactNode;
}

const LogoLayout: React.FC<ILogoLayoutProps> = ({ children }) => {
  return (
    <>
      <div className="logo">Y-Chat</div>
      {children}
    </>
  );
};

export default LogoLayout;
