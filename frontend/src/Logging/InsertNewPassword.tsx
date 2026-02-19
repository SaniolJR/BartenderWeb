import { useState } from 'react';
import { useForm } from 'react-hook-form';
import { TextField, Button, Stack, IconButton } from "@mui/material";
import { Visibility, VisibilityOff } from "@mui/icons-material"
import {z} from 'zod';
import { zodResolver } from '@hookform/resolvers/zod';


const changePasswordSchema = z.object({

    //schema what new password should have
    NewPassword: z.string()
        .min(8, "Password must contains at least 8 characters")
        .regex(/[A-Z]/, "Password must contains al least 1 big letter")
        .regex(/[a-z]/, "Password must contains al least 1 small letter")
        .regex(/[0-9]/, "Password must contains al least 1 number")
        .regex(/[!@#$%^&*(),.?":{}|<>]/, "Password must contain at least 1 special character"),
    NewPasswordConfirm: z.string()
    })
    .refine((data) => data.NewPassword === data.NewPasswordConfirm, {
        message: "Passwords are different",
        path: ["NewPasswordConfirm"]    //send message to NewPasswoerdConfirm textfield
    })

    interface InsertPasswordProps {
        onPasswordChange: (password: string) => void;
    }

    export default function InsertNewPassword({ onPasswordChange }: InsertPasswordProps){
   
        //usestates for diplaying password texts
    const [showPassword, setShowPassword] = useState(false);
    const [showPasswordConfirm, setShowPasswordConfirm] = useState(false);
        
    //except of useng use state, i will use useForm here
    //it allows me to menage input values, form falidation and errors and form sending at once

    const {register, handleSubmit, formState: {errors} } = useForm({
        resolver: zodResolver(changePasswordSchema) //RFH will use here my zod schema for validation
    });

    
    return (
        
            <form onSubmit={handleSubmit((data) => {
                // if password form is valid and its confirmation is valid
                onPasswordChange(data.NewPassword)  //change parent's data
            })}>
            <Stack spacing={2}>
                <TextField
                    label="Insert new password"
                    {...register("NewPassword")}    //register value in RHF
                    error={!!errors.NewPassword}    //check for errors in zod (pointed by RFH)
                    helperText={errors.NewPassword?.message}    //get message from zod

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
                        "& input": { fontSize: "3vh" },
                        "& label": { fontSize: "2vh" },
                        "& .MuiFormHelperText-root": { fontSize: "2vh", fontWeight: "bold" }
                    }}
                    
                />
                <TextField
                    label="Confirm new password"
                    {...register("NewPasswordConfirm")} //register value in RHF
                    error={!!errors.NewPasswordConfirm}    //check for errors in zod (pointed by RFH)
                    helperText={errors.NewPasswordConfirm?.message}    //get message from zod

                    //hide text and add button that makes it visible
                    type={showPasswordConfirm ? "text" : "password"}
                    InputProps={{
                        endAdornment: (
                            <IconButton onClick={() => setShowPasswordConfirm(!showPasswordConfirm)}>
                                {showPasswordConfirm ? <VisibilityOff /> : <Visibility />}
                            </IconButton>
                        )
                    }}

                    sx={{ 
                        "& input": { fontSize: "3vh" },
                        "& label": { fontSize: "2vh" },
                        "& .MuiFormHelperText-root": { fontSize: "2vh", fontWeight: "bold" }
                    }}
                />

                <Button sx={{ fontSize: "3vh" }} type="submit">Change password</Button>
         </Stack>
        </form>
    )
}


