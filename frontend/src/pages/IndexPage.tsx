import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import { FetchIndexData } from "../utils";

const IndexPage: React.FC = () => {
  const navigate = useNavigate();
  const [data, setData] = useState<any>(null);

  useEffect(() => {
    FetchIndexData()
      .then((data) => {
        console.log(data);
        setData(data);
      })
      .catch((error) => {
        console.log(error.message);
        if (error.message === "Email is not confirmed") {
          navigate("/confirm-email");
        } else if (error.message === "Profile details not added") {
          navigate("/add-profile-details");
        }
      });
  }, []);

  return (
    <>
      {data && data.message}
      <div
        onClick={() => {
          localStorage.removeItem("token");
          navigate("/login");
        }}
      >
        Logout
      </div>
    </>
  );
};

export default IndexPage;
