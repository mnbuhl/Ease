import { useEffect } from 'react';
import toast, { Toaster } from 'react-hot-toast';
import { Toast } from '../models/toast';

const Toast = ({ message, type, duration }: Toast) => {
  useEffect(() => {
    if (!message) return;

    console.log(message, type);

    if (type === 'error') {
      toast.error(message, { duration });
    } else if (type === 'success') {
      toast.success(message, { duration });
    } else {
      toast(message, { duration });
    }
  }, [message, type, duration]);

  return <Toaster />;
};

export default Toast;
