import { LabelHTMLAttributes } from 'react';
import { twMerge } from 'tailwind-merge';

export default function Label({
  value,
  className = '',
  children,
  ...props
}: LabelHTMLAttributes<HTMLLabelElement> & { value?: string }) {
  return (
    <label
      {...props}
      className={twMerge('block font-medium text-sm text-gray-700 dark:text-gray-300', className)}
    >
      {value ? value : children}
    </label>
  );
}
