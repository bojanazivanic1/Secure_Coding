import './App.css';
import { AuthContextProvider } from './contexts/auth-context';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Navbar from './components/Navbar/Navbar';
import AppRouter from './router/Router';

function App() {
  return (
    <AuthContextProvider>
      <ToastContainer position="top-center" />
      <Navbar />
      <AppRouter />
    </AuthContextProvider>
  )
}

export default App
