import { createTheme } from '@mui/material/styles'

const field = {
  bg: 'rgba(102, 4, 4, 0.35)',
  border: '#9c9c00',
  main: '#eeee6c',
  hoverBg: 'rgba(156,156,0,0.12)',
  focusRing: 'rgba(238, 238, 108, 0.18)',
}

export const theme = createTheme({
  palette: { 
    primary: {
      main: '#eeee6c', // Twój kolor (np. zielony) zamiast niebieskiego
    },
    mode: 'dark' 
  },
  

  components: {
    // GLOBAL - all TextFields and component thats includes Textfields
    MuiTextField: {
      defaultProps: {
        variant: 'outlined',
        size: 'small',
      },
    },
    //Textfield inclutes:
    MuiInputLabel: {
      styleOverrides: {
        root: {
          color: field.border,
          fontWeight: 700,
        },
        focused: {
          color: field.main,
        },
      },
    },
    //Textfield inclutes:
    MuiOutlinedInput: {
      styleOverrides: {
        root: {
          backgroundColor: field.bg,
          borderRadius: 12,

          '& .MuiOutlinedInput-notchedOutline': {
            borderColor: field.border,
          },
          '&:hover .MuiOutlinedInput-notchedOutline': {
            borderColor: field.main,
          },
          '&.Mui-focused .MuiOutlinedInput-notchedOutline': {
            borderColor: field.main,
          },
          '&.Mui-focused': {
            boxShadow: `0 0 0 2px ${field.focusRing}`,
          },
        },

        input: {
          color: field.main,
          fontWeight: 700,

          '&::placeholder': {
            color: 'rgba(238, 238, 108, 0.55)',
            opacity: 1,
          },

          // arrows for number type
          '&[type=number]::-webkit-inner-spin-button, &[type=number]::-webkit-outer-spin-button':
            {
              transform: 'scale(1.6)',
              transformOrigin: 'right center',
            },
        },
      },
    },

    MuiFormHelperText: {
      styleOverrides: {
        root: {
          color: 'rgba(238, 238, 108, 0.65)',
        },
      },
    },

    // BUTTONS
    MuiButton: {
      styleOverrides: {
        root: {
          textTransform: 'none',
          fontWeight: 700,
          borderRadius: 12,
        },
      },
      //VARIANTS OF BUTTONS
      variants: [
        //NAVBAR VARIANT
        {
          props: { 'data-variant': 'nav' } as any,
          style: {
            color: field.border,
            paddingLeft: 16,
            paddingRight: 16,
            fontSize: '2vh',
            '&:hover': {
              backgroundColor: field.hoverBg,
              boxShadow: '0 0 10px #9c9c00',
            },
          },
        },
        //NAVBAR MAIN ELEMENTS VARAINT
        {
          props: { 'data-variant': 'nav', 'data-tone': 'main' } as any,
          style: {
            color: field.main,
            fontSize: '3vh',
            '&:hover': { boxShadow: '0 0 12px #eeee6c' },
          },
        },
      ],
    },
  },
})