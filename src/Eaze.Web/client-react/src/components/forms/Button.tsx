import { ButtonHTMLAttributes } from 'react';
import { twMerge } from 'tailwind-merge';

interface Props extends ButtonHTMLAttributes<HTMLButtonElement> {
  children: React.ReactNode;
  type?: 'button' | 'submit' | 'reset';
  variant?: 'primary' | 'secondary' | 'danger' | 'text';
  loading?: boolean;
}

const Button = ({
  children,
  className = '',
  loading,
  disabled,
  variant = 'primary',
  ...props
}: Props) => {
  let classes =
    'inline-flex items-center px-4 py-2 border border-transparent rounded-md font-semibold text-xs uppercase tracking-widest focus:outline-none focus:ring-2 focus:ring-offset-2 transition ease-in-out duration-150 disabled:opacity-50 disabled:cursor-not-allowed';

  switch (variant) {
    case 'primary':
      classes = twMerge(
        classes,
        'bg-gray-800 dark:bg-gray-200 text-white dark:text-gray-800, hover:bg-gray-700 dark:hover:bg-white focus:bg-gray-700 dark:focus:bg-white active:bg-gray-900 dark:active:bg-gray-300 focus:ring-indigo-500 dark:focus:ring-offset-gray-800'
      );
      break;
    case 'secondary':
      classes = twMerge(
        classes,
        'bg-gray-200 dark:bg-gray-700 text-gray-800 dark:text-gray-100 hover:bg-gray-300 dark:hover:bg-gray-600 focus:bg-gray-200 dark:focus:bg-gray-600 active:bg-gray-400 dark:active:bg-gray-500 focus:ring-indigo-500 dark:focus:ring-offset-gray-800'
      );
      break;
    case 'danger':
      classes = twMerge(
        classes,
        'bg-red-600 dark:bg-red-400 text-white dark:text-red-600 hover:bg-red-500 dark:hover:bg-red-300 focus:bg-red-500 dark:focus:bg-red-300 active:bg-red-700 dark:active:bg-red-500 focus:ring-red-500 dark:focus:ring-offset-gray-800'
      );
      break;
    case 'text':
      classes = twMerge(
        classes,
        'bg-transparent text-gray-800 dark:text-gray-100 hover:bg-gray-100 dark:hover:bg-gray-600 focus:bg-gray-100 dark:focus:bg-gray-600 active:bg-gray-200 dark:active:bg-gray-500 focus:ring-indigo-500 dark:focus:ring-offset-gray-800'
      );
      break;
  }

  return (
    <button disabled={loading || disabled} {...props} className={twMerge(classes, className)}>
      {children}
    </button>
  );
};

export default Button;
