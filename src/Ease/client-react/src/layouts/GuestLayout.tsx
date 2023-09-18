import { Link, usePage } from '@inertiajs/react';
import { PropsWithChildren } from 'react';
import ApplicationLogo from '../assets/ApplicationLogo';
import { PageProps } from '../page-props';
import Toast from '../components/Toast';

const GuestLayout = ({ children }: PropsWithChildren) => {
  const { toast } = usePage<PageProps>().props;
  return (
    <div className="min-h-screen flex flex-col sm:justify-center items-center pt-6 sm:pt-0 bg-gray-100 dark:bg-gray-900">
      <div>
        <Link href="/">
          <ApplicationLogo className="rounded-lg w-24 h-24 fill-current" />
        </Link>
      </div>

      <div className="w-full sm:max-w-md mt-6 px-6 py-4 bg-white dark:bg-gray-800 shadow-md overflow-hidden sm:rounded-lg">
        {children}
      </div>
      <Toast message={toast?.message} type={toast?.type} />
    </div>
  );
};

export default GuestLayout;
