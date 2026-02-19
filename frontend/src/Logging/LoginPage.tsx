import { useState } from 'react'
import { Button, Stack, TextField, Typography, IconButton, Container } from '@mui/material'
import { Visibility, VisibilityOff } from '@mui/icons-material'
import { Link, useNavigate } from 'react-router-dom'
import type { UserType } from "../types";


export default function LoginPage({
  onLoginSuccess,
  setUser,
}: {
  onLoginSuccess: () => void;
  setUser: (user: UserType) => void;
}) {
    const [username, setUsername] = useState<string>("");
    const [password, setPassword] = useState<string>("");
    const [showPassword, setShowPassword] = useState<boolean>(false);
    const [error, setError] = useState<string>("");
    const navigate = useNavigate();

    const API_URL = import.meta.env.VITE_API_URL    //link to backend from .env

    //connection to backend 
    const loginRequest = async () => {
        try
        {
            const response = await fetch(`${API_URL}/auth/login`, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    Username: username,
                    Password: password
                }),
                // send cookies do server and save response cookies from server
                credentials: "include"  
            });

            if(response.ok){
                //logged - change navbar and navigate to user
                onLoginSuccess();
                const user: UserType = await response.json();
                setUser(user);
                navigate("/account");
            }
            else{
                //return server message if unsuccesful
                const data = await response.json()
                setError(data.message)
            }
        }
        catch{
            setError("Cannot connect to server")
        }
    }

    return (
        <Container maxWidth="lg">
            <Stack spacing={5}>
            <Typography variant="h1" sx={{ textAlign: "center", 
                            fontWeight: 900, 
                            mb: 1, 
                            fontSize: "8vh" }}>
                Login to your account
            </Typography>

            <TextField
            label="Insert username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            sx={{ 
                "& input": { fontSize: "6vh" },
                "& label": { fontSize: "4vh" },
                "& .MuiFormHelperText-root": { fontSize: "2vh", fontWeight: "bold" }
                }}
            />

            <TextField
            label="Insert password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            //hide text and add button that makes it visible
                type={showPassword ? "text" : "password"}
                InputProps={{
                    endAdornment: (
                        <IconButton onClick={() => setShowPassword(!showPassword)}>
                            {showPassword ? <VisibilityOff /> : <Visibility />}
                        </IconButton>
                    )
                }}
                sx={{ 
                    "& input": { fontSize: "6vh" },
                    "& label": { fontSize: "4vh" },
                    "& .MuiFormHelperText-root": { fontSize: "2vh", fontWeight: "bold" }
                }}
            />
            
            <Button fullWidth variant="outlined" 
                    onClick={loginRequest} sx={{ fontSize: "4vh" }}>
                Login
            </Button>

            <Typography color="error" sx={{ fontSize: "4vh", fontWeight: "bold", textAlign: "center" }}>
                {error}
            </Typography>

            <Link to="/register" style={{ display: "block" }}>
            <Button  fullWidth variant="outlined" sx={{ fontSize: "4vh" }}>
                Dont have account? Register it here!
            </Button>
            </Link>

         </Stack>
    </Container>
  )
}