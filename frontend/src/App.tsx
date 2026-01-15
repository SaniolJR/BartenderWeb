import { Container, Typography } from '@mui/material'
import { Navigate, Route, Routes } from 'react-router-dom'
import NavBar from './NavBar'
import BartenderWebPage from './BartenderWeb/BartenderWebPage'
import SearchDrinkPage from './SearchDrink/SearchDrinkPage'
import AddDrinkPage from './AddDrink/AddDrinkPage'
import FavDrinksPage from './FavDrinks/FavDrinksPage'
import AccPage from './Account/AccPage'
import LogoutPage from './Logging/LogoutPage'



export default function App() {
  return (
    <>
      <NavBar />

      <Routes>
        <Route path="/" element={<BartenderWebPage />} />
        <Route path="/search" element={<SearchDrinkPage/>} />
        <Route path="/add" element={<AddDrinkPage/>} />
        <Route path="/favourites" element={<FavDrinksPage/>} />
        <Route path="/account" element={<AccPage/>} />
        <Route path="/logout" element={<LogoutPage/>} />

        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    </>
  )
}