
import { useState, type MouseEvent } from 'react'
import { AppBar, Box, Button, Menu, MenuItem, Toolbar } from '@mui/material'
import { Link as RouterLink } from 'react-router-dom'

const navItems = [
  { label: 'Search', to: '/search' },
  { label: 'Add drink', to: '/add' },
  { label: 'Favourites Drinks', to: '/favourites' },
  { label: 'Account', to: '/account' },
  { label: 'Logout', to: '/logout' },
] as const

export default function NavBar() {
  //State to handle Menu dropwon on phones
  const [dropEL, setDropEL] = useState<HTMLElement | null>(null)
  const menuOpen = Boolean(dropEL)

  const handleOpenMenu = (event: MouseEvent<HTMLButtonElement>) => {
    setDropEL(event.currentTarget)
  }

  const handleCloseMenu = () => {
    setDropEL(null)
  }
  
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
          justifyContent: 'space-between',
          
        }}>

          <Button data-variant="nav" data-tone="main">
            BartenderWeb
          </Button>
         
         
         {/*DESKTOP VERSION*/}
         <Box sx={{ display: { xs: 'none', md: 'flex' }, gap: 1, flexGrow: 1, justifyContent: 'space-evenly' }}>
          {navItems.map((item) => (
            <Button
              key={item.to}
              component={RouterLink}
              to={item.to}
              data-variant="nav"
            >
              {item.label}
            </Button>
          ))}
        </Box>      

        
         {/*PHONE VERSION*/} 
         <Box sx={{ display: { xs: 'inline-flex', md: 'none' } }}>
          <Button data-variant="nav" data-tone="main" onClick={handleOpenMenu}>
            Menu
          </Button>

          <Menu anchorEl={dropEL} open={menuOpen} onClose={handleCloseMenu}>
            {navItems.map((item) => (
              <MenuItem
                key={item.to}
                component={RouterLink}
                to={item.to}
                onClick={handleCloseMenu}
              >
                {item.label}
              </MenuItem>
            ))}
          </Menu>
         </Box> 
          

      </Toolbar>
    </AppBar>
  )
}