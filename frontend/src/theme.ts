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
            '&:hover': {
              backgroundColor: 'rgba(156,156,0,0.12)',
              boxShadow: '0 0 10px #9c9c00',
            },
            fontSize: '2vh'
          },
        },
        {
           props: { 'data-variant': 'nav', 'data-tone': 'main' } as any,
            style: {
              color: '#eeee6c',
              '&:hover': { boxShadow: '0 0 12px #eeee6c)' },
              fontSize: '3vh'
            },
        }
      ]

    }

  }
})