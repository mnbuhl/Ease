import { useEffect } from 'react';
import toast, { Toaster } from 'react-hot-toast';

const Toast = ({ message, type }: Toast) => {
  useEffect(() => {
    if (!message) return;

    console.log(message, type);

    if (type === 'error') {
      toast.error(message);
    } else if (type === 'success') {
      toast.success(message);
    } else {
      toast(message);
    }
  }, [message, type]);

  return <Toaster />;
};

export default Toast;
