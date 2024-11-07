import {Route, Routes} from "react-router-dom";
import {ROUTES} from "./imports.ts";
import LoginComponent from "./LoginComponent.tsx";
import UserListComponent from "./User/UserListComponent.tsx";
import RegisterComponent from "./User/RegisterComponent.tsx";


function App() {


  return (
    <>
        <Routes>
            <Route path={ROUTES.LOGIN} element={<LoginComponent />}/>
            <Route path={ROUTES.USERS} element={<UserListComponent/>}/>
            <Route path={ROUTES.REGISTER} element={<RegisterComponent/>}/>
        </Routes>

    </>
  )
}

export default App
