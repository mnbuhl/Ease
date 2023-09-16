import { InputHTMLAttributes, forwardRef, useRef, useImperativeHandle, useEffect } from 'react';
import { twMerge } from 'tailwind-merge';

type Props = InputHTMLAttributes<HTMLInputElement> & {
  isFocused?: boolean;
};

const Input = forwardRef(({ isFocused, className, type = 'text', ...props }: Props, ref) => {
  const localRef = useRef<HTMLInputElement>(null);

  useImperativeHandle(ref, () => ({
    onFocus: () => localRef.current?.focus(),
  }));

  useEffect(() => {
    if (isFocused) {
      localRef.current?.focus();
    }
  }, [isFocused]);

  return (
    <input
      {...props}
      className={twMerge(
        'border-gray-300 dark:border-gray-700 dark:bg-gray-900 dark:text-gray-300 focus:border-indigo-500 dark:focus:border-indigo-600 focus:ring-indigo-500 dark:focus:ring-indigo-600 rounded-md shadow-sm',
        className
      )}
      type={type}
      ref={localRef}
    />
  );
});

export default Input;
