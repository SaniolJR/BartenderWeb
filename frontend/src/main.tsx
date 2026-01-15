import { createRoot } from 'react-dom/client'
import App from './App'
import { CssBaseline, ThemeProvider } from '@mui/material'
import { theme } from './theme'

const rootEl = document.getElementById('root')
if (!rootEl) throw new Error('Brak elementu #root w index.html')

createRoot(rootEl).render(
  <ThemeProvider theme={theme}>
    <CssBaseline />
    <App />
  </ThemeProvider>
)