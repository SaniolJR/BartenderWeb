
import { useState } from 'react';
import { useForm } from 'react-hook-form';
import { TextField, Button, Stack, Container, Typography } from "@mui/material";
import {z} from 'zod';
import { zodResolver } from '@hookform/resolvers/zod';
import InsertNewPassword from "./InsertNewPassword";
import { useNavigate } from "react-router-dom";

const registerSchema = z.object({
    Username: z.string()
    .min(8, "Username is required"),
    Email: z.string()
    .min(8, "Email is required")
    .email("Invalid email address"),
})

const API_URL = import.meta.env.VITE_API_URL    //link to backend from .env


export default function LoginPage(){
    const navigate = useNavigate();
    const [password, setPassword] = useState("");
    const [successMsg, setSuccessMsg] = useState("");
    const [errorMsg, setErrorMsg] = useState("");

    const { register, handleSubmit, formState: { errors } } = useForm({
        resolver: zodResolver(registerSchema)
    });

    const onSubmit = async (data: any) => {
        try{
        const response = await fetch(`${API_URL}/auth/registration`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                Username: data.Username,
                Password: password, // from InsertNewPassword
                Email: data.Email
            })
        });
            if (response.ok) {
                setSuccessMsg("Account created successfully!");
                setTimeout(() => {
                setSuccessMsg("");
                navigate("/login"); // navigate to /login
                }, 2000); // 2 seconds popup
            } else {
                const msg = await response.text();
                setErrorMsg(msg || "Registration failed");
            }
        }
        catch(error){
            setErrorMsg("Cannot connect to server");
            console.error(error);
        }
    }

    return(
        <Container sx={{ mt: 8 }}>
            <form onSubmit={handleSubmit(onSubmit)}>
            <Stack sx={{ mb: 2 }} spacing={2}>

                {successMsg && (
                    <Typography sx={{ 
                            color: "green",
                            fontWeight: "bold",
                            textAlign: "center", 
                            mb: 2,
                            fontSize: "3vh" }}>
                        {successMsg}
                    </Typography>
                    )}
                    {errorMsg && (
                    <Typography sx={{
                            color: "red",
                            fontWeight: "bold",
                            textAlign: "center", 
                            mb: 2,
                            fontSize: "3vh" }}>
                        {errorMsg}
                    </Typography>
                )}

                {password && (<Button
                        sx={{ fontSize: "3vh", display: "block", mx: "auto" }}
                        type="submit">
                        Create account
                    </Button>
                    )}

                <TextField
                        label="Insert new username"
                        {...register("Username")}    //register value in RHF
                        error={!!errors.Username}    //check for errors in zod (pointed by RFH)
                        helperText={errors.Username?.message}    //get message from zod
                        sx={{ 
                            "& input": { fontSize: "3vh" },
                            "& label": { fontSize: "2vh" },
                            "& .MuiFormHelperText-root": { fontSize: "2vh", fontWeight: "bold" }
                        }}
                />
                <TextField
                        label="Insert new email"
                        {...register("Email")}    //register value in RHF
                        error={!!errors.Email}    //check for errors in zod (pointed by RFH)
                        helperText={errors.Email?.message}    //get message from zod
                        sx={{ 
                            "& input": { fontSize: "3vh" },
                            "& label": { fontSize: "2vh" },
                            "& .MuiFormHelperText-root": { fontSize: "2vh", fontWeight: "bold" },
                        }}
                />
            
            </Stack>
            </form>
            <InsertNewPassword onPasswordChange={setPassword} />
        </Container>
    )
}