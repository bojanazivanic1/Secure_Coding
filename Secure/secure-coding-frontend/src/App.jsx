import { AuthContextProvider } from './contexts/auth-context';
import { QrContextProvider } from './contexts/qr-context';
import AppRouter from './router/Router';
import Navbar from './components/Navbar/Navbar';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import './App.css';

const App = () => {
  return (
    <AuthContextProvider>
      <QrContextProvider>
        <ToastContainer position="top-center" />
        <Navbar />
        <AppRouter />
      </QrContextProvider>
    </AuthContextProvider>  
  );
};

export default App;
