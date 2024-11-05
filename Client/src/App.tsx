import {Route, Routes} from "react-router-dom";
import {ROUTES} from "./Helpers/Constants/Routes.ts";
import LoginComponent from "./Components/LoginComponent.tsx";
import UserListComponent from "./Components/User/UserListComponent.tsx";


function App() {


  return (
    <>
        <Routes>
            <Route path={ROUTES.LOGIN} element={<LoginComponent />}/>
            <Route path={ROUTES.USERS} element={<UserListComponent/>}/>
        </Routes>

    </>
  )
}

export default App
