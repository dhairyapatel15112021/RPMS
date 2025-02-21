
import { Outlet } from 'react-router-dom';
import './App.css';
import Header from './component/Header';
import store from './store/Store';
import { Provider } from 'react-redux';
import { UseAuthCheck } from './component/UseAuthCheck';
import { useEffect, useState } from 'react';
import {Toaster} from "react-hot-toast";
function App() {
  const [isChecked, setIsChecked] = useState(false);
  return (
    <Provider store={store}>
      <Toaster/>
      <UseAuthCheck setIsChecked={setIsChecked} />
      {
        isChecked &&
        <>
          <Header />
          <Outlet />
        </>
      }
    </Provider>
  )
}

export default App