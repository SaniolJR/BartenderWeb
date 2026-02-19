import { Container, Typography } from '@mui/material'
import { Navigate, Route, Routes } from 'react-router-dom'
import NavBar from './NavBar'
import BartenderWebPage from './BartenderWeb/BartenderWebPage'
import SearchDrinkPage from './SearchDrink/SearchDrinkPage'
import AddDrinkPage from './AddDrink/AddDrinkPage'
import FavDrinksPage from './FavDrinks/FavDrinksPage'
import AccPage from './Account/AccPage'
import LogoutPage from './Logging/LogoutPage'
import LoginPage from './Logging/LoginPage'
import RegisterPage from './Logging/RegisterPage'
import AddIngredientPage from './AddIngredient/AddIngredientPage'
import DrinksDetailsPage from './SearchDrink/DrinksDetailsPage';
import { useState, useEffect } from 'react'
import UseRefreshToken from "./Logging/refreshToken";
import type { UserType } from "./types";



export default function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [user, setUser] = useState<UserType | null>(null);

  const refreshToken = UseRefreshToken(setIsLoggedIn);
  
  //uing refresh token func to interval refresh
  useEffect( () => {
    if(!isLoggedIn) return;
    
    const interval = setInterval(() => {
      refreshToken();
    }, 4 * 60 * 1000);  //every 4 min
    
    return () => clearInterval(interval);
  }, [isLoggedIn, refreshToken]);

  return (
    <>
      <NavBar isLoggedIn={isLoggedIn}/>

      <Routes>
        <Route path="/" element={<BartenderWebPage />} />
        <Route path="/search" element={<SearchDrinkPage/>} />
        <Route path="/addDrink" element={<AddDrinkPage/>} />
        <Route path="/addIngredient" element={<AddIngredientPage/>} />
        <Route path="/favourites" element={<FavDrinksPage/>} />
        <Route path="/account" element={<AccPage user={user} />} />
        <Route
          path="/logout"
          element={<LogoutPage isLoggedIn={isLoggedIn} setIsLoggedIn={setIsLoggedIn} />}
        />
        <Route path="/drink/:id" element={<DrinksDetailsPage />} />
        <Route path="/login" element={<LoginPage 
            onLoginSuccess={() => setIsLoggedIn(true)}
            setUser={setUser} />} />
        <Route path="/register" element={<RegisterPage/>}/>

        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    </>
  )
}