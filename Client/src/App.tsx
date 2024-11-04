import {Route, Routes} from "react-router-dom";
import {ROUTES} from "./Constants/Routes.ts";
import LoginComponent from "./Components/LoginComponent.tsx";


function App() {


  return (
    <>
        <Routes>
            <Route path={ROUTES.LOGIN} element={<LoginComponent />}/>
        </Routes>

    </>
  )
}

export default App
