import { AppBar, Box, Button, Toolbar, Typography } from '@mui/material'

const navItems: string[] = ['Search', 'Add drink', 'Favourites Drinks', 'Account', 'Logout']

export default function NavBar() {
    //BartenderApp as a title.
  return (
    <AppBar position="static" 
      sx={{ 
          bgcolor: '#660404',
          border: '0.5vh solid #9c9c00' 
        }}>
      <Toolbar sx={{ 
          height: '10vh',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'space-evenly'
        }}>

          <Button>
            Bartender
          </Button>
         
          {navItems.map((label) => (
            <Button data-variant="nav">
              {label}
            </Button>
          ))}         
          

      </Toolbar>
    </AppBar>
  )
}