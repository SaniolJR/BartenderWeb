import { Box, Button, Container, TextField, Stack, Typography } from '@mui/material'
import { Link as RouterLink } from 'react-router-dom'
import type { UserType } from '../types'
import { useState } from 'react';
import InsertNewPassword from "../Logging/InsertNewPassword";

type AccPageProps = {
  user: UserType | null;
}

export default function AccPage({ user }: AccPageProps) {
const [changePasswordOption, showChangePasswordOption] = useState(false);
const [oldPassword, setOldPassword] = useState("");
const [changeMsg, setChangeMsg] = useState("");

const API_URL = import.meta.env.VITE_API_URL    //link to backend from .env

  //if user is not returned - shouldnt happen or only if somebody use direct link withour logging
  if (!user) return (
      <Typography variant="h5" sx={{ mt: 4, textAlign: "center", fontSize: "3vh" }}>
        You must be logged in to view account page
      </Typography>
  );


  return (
    <Container maxWidth="sm" sx={{ mt: 6 }}>
        <Typography variant="h3" sx={{ fontWeight: 900, mb: 3, textAlign: "center" }}>
          Manage your Account
        </Typography>
        <Stack spacing={3}>
          <Box>
            <Typography variant="h6" sx={{ fontWeight: 700, fontSize: "3vh" }}>
                Username:
            </Typography>
            <Typography variant="body1" sx={{fontSize: "3vh" }}>
              {user.username}
            </Typography>
          </Box>
          <Box>
            <Typography variant="h6" sx={{ fontWeight: 700, fontSize: "3vh" }}>
              Email:
            </Typography>
            <Typography variant="body1" sx={{fontSize: "3vh" }}>
              {user.email}
            </Typography>
          </Box>

          {!changePasswordOption && (
            <Button variant="outlined"
            color="primary"
            sx={{ mt: 2, borderRadius: 2, fontSize: "3vh" }}
            onClick={() => showChangePasswordOption(true)}>
              Change your password here
            </Button>
          )}

          {changePasswordOption && (
            <Box>
              <TextField
                label="Old password"
                type="password"
                value={oldPassword}
                onChange={e => setOldPassword(e.target.value)}
                sx={{ 
                        "& input": { fontSize: "3vh" },
                        "& label": { fontSize: "2vh" },
                        "& .MuiFormHelperText-root": { fontSize: "2vh", fontWeight: "bold" },
                        mb: 2
                    }}
              />
              {/* Change password options: */
                <InsertNewPassword onPasswordChange={async (newPassword) => {
                  try{
                    const response = await fetch(`${API_URL}/auth/update-password`, {
                        method: "POST",
                        headers: { "Content-Type": "application/json" },
                        body: JSON.stringify({
                            OldPassword: oldPassword,
                            NewPassword: newPassword
                      }),
                      // send cookies do server and save response cookies from server
                      credentials: "include"  
                    });
                      if(response.ok){
                        setChangeMsg("Password changed successfully!");
                        setTimeout(() => setChangeMsg(""), 5000);
                        setOldPassword("");
                        showChangePasswordOption(false);
                      }
                    else{
                        //return server message if unsuccesful
                        const data = await response.json()
                        setChangeMsg(data.message)
                    }
                  }
                  catch{
                    setChangeMsg("Cannot connect to server")
                    setTimeout(() => setChangeMsg(""), 5000);
                  }
                }} />
              }
              <Button sx={{ fontSize: "3vh", display: "block", mx: "auto" }} 
                onClick={() => { 
                  showChangePasswordOption(false); 
                  setChangeMsg("");
                  setTimeout(() => setChangeMsg(""), 5000);}}>
                Cancel
              </Button>
            </Box>
          )}

          <Button
            variant="outlined"
            color="primary"
            component={RouterLink}
            to="/"
            sx={{ mt: 2, borderRadius: 2, fontSize: "3vh" }}
          >
            Back to Home
          </Button>

          
          {changeMsg && (
            <Typography
              sx={{
                fontWeight: "bold",
                textAlign: "center",
                mb: 2,
                fontSize: "3vh",
                color: "#FFD700",
                textDecoration: "underline"
              }}
            >
              {changeMsg}
            </Typography>
          )}

        </Stack>
    </Container>
  )
}

