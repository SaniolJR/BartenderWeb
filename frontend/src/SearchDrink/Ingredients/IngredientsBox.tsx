import { Box } from '@mui/material'

export default function IngredientsBox() {
  return (
    <Box sx={{ 
      bgcolor: 'rgba(102, 4, 4, 0.2)',
      border: '1px solid #9c9c00',
      borderRadius: 2,
      p: 2,
      minHeight: '400px'
    }}>
      <h3>Ingredients</h3>
    </Box>
  )
}