import { createTheme } from '@mui/material/styles'
import { indigo, pink } from '@mui/material/colors'

export const theme = createTheme({
  palette: { mode: 'dark' },
  //overrides
  components: {
    //buttons
    MuiButton: {
      //for all buttons
      styleOverrides:{
        root: {
          textTransform: 'none',
          fontWeight: 700,    //bold font
          borderRadius: 12
        },
      },
      //  ==variants of ovverides==
      variants: [
        {
          //  <Button data-variant="navBar"/>
          props: { 'data-variant': 'nav' } as any,
          style: {
            color: '#9c9c00',
            paddingLeft: 16,
            paddingRight: 16,
          },
        }
      ]

    }

  }
})