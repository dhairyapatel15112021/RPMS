
import { Outlet } from 'react-router-dom';
import './App.css';
import Header from './component/Header';
import store from './store/Store';
import { Provider } from 'react-redux';

function App() {

  return (

    <Provider store={store}>
      <Header />
      <Outlet />
    </Provider>
  )
}

export default App