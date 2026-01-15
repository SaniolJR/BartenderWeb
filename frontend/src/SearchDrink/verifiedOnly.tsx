import type { Dispatch, SetStateAction, ChangeEvent } from 'react'
import { FormControlLabel, Checkbox } from '@mui/material'

type Props = {
    verified: boolean
    setVerified: Dispatch<SetStateAction<boolean>>
}

export default function VerifiedOnly({ verified, setVerified }: Props) {
    const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
        setVerified(e.target.checked)
    }

    return (
        <FormControlLabel
            label="Verified only"
            labelPlacement="start"
            value = "start"
            sx={{
                '& .MuiFormControlLabel-label': {
                marginRight: 0,
                }
            }}
            control={
            <Checkbox 
                checked={verified} 
                onChange={handleChange}
                sx={{ '&.Mui-checked': { color: 'green' } }} 
                />
            }
        />
    )
}