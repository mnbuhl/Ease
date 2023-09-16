import { HTMLAttributes } from 'react';
import { twMerge } from 'tailwind-merge';

export default function Error({
  message,
  className = '',
  ...props
}: HTMLAttributes<HTMLParagraphElement> & { message?: string }) {
  return message ? (
    <p {...props} className={twMerge('text-sm text-red-600 dark:text-red-400', className)}>
      {message}
    </p>
  ) : null;
}
