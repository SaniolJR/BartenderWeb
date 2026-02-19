import { Box, Button, Container, Paper, Stack, Typography } from '@mui/material'
import { Link as RouterLink } from 'react-router-dom'
import InsertPassword from './InsertNewPassword'


export default function LoginRegisterPage(){
    return (
    <Container>
        <Box>
          <Typography variant="h3" sx={{ fontWeight: 900, mb: 1 }}>
            Login to your account
          </Typography>
          
        </Box>

    </Container>
  )
}