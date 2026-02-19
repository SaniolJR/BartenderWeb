import { useNavigate } from "react-router-dom";


export default function UseRefreshToken(setIsLoggedIn: (v: boolean) => void) {
  const navigate = useNavigate();
    async function refreshToken(){
        try{
            const response = await fetch(`${import.meta.env.VITE_API_URL}/auth/refresh`,{
                method: "POST",
                credentials: "include"
            });

            if(response.ok){
                //token is refreshed
                return true;
            }
            else{
                //Refresh token has expired, logout and navigate to login page
                setIsLoggedIn(false);      // clear login state
                navigate("/login");        // navigate to login page
                return false;
            }
        }
        catch(err){
            //connection error or sth - thread as logout
            setIsLoggedIn(false);
            navigate("/login");
            return false;
        }
    }

    return refreshToken;
}