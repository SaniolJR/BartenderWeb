import { useEffect, useState } from "react";
import { Box, Container, Typography } from '@mui/material';

type LogoutPageProps = {
  isLoggedIn: boolean;
  setIsLoggedIn: (value: boolean) => void;
};

export default function LogoutPage({ isLoggedIn, setIsLoggedIn }: LogoutPageProps) {
  const [msg, setMsg] = useState("");

  useEffect(() => {
    if (isLoggedIn) {
      fetch(`${import.meta.env.VITE_API_URL}/auth/logout`, {
        method: "POST",
        credentials: "include"
      }).finally(() => {
        setIsLoggedIn(false);
        setMsg("You have been logged out");
      });
    } else {
      setMsg("You can't log out without being logged in");
    }
  }, [isLoggedIn, setIsLoggedIn]);

  return (
    <Container>
      <Box>
        <Typography variant="h3" sx={{ fontWeight: 900, mb: 1, fontSize: "3vh" }}>
          {msg}
        </Typography>
      </Box>
    </Container>
  );
}