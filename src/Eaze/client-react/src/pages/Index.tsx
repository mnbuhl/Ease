import { Head, Link } from '@inertiajs/react';
import { PageProps } from '../page-props';

const Index = ({ auth }: PageProps) => {
  return (
    <>
      <Head title="Index" />
      <div className="relative sm:flex sm:justify-center sm:items-center min-h-screen bg-dots-darker bg-center bg-gray-100 dark:bg-dots-lighter dark:bg-gray-900 selection:bg-indigo-500 selection:text-white">
        <nav className="sm:fixed sm:top-0 sm:right-0 p-6 text-right">
          {auth ? (
            <Link
              href={'/dashboard'}
              className="font-semibold text-gray-600 hover:text-gray-900 dark:text-gray-400 dark:hover:text-white focus:outline focus:outline-2 focus:rounded-sm focus:outline-indigo-500"
            >
              Dashboard
            </Link>
          ) : (
            <>
              <Link
                href={'/auth/login'}
                className="font-semibold text-gray-600 hover:text-gray-900 dark:text-gray-400 dark:hover:text-white focus:outline focus:outline-2 focus:rounded-sm focus:outline-indigo-500"
              >
                Log in
              </Link>

              <Link
                href={'auth/register'}
                className="ml-4 font-semibold text-gray-600 hover:text-gray-900 dark:text-gray-400 dark:hover:text-white focus:outline focus:outline-2 focus:rounded-sm focus:outline-indigo-500"
              >
                Register
              </Link>
            </>
          )}
        </nav>
        <main>To be designed</main>
      </div>
    </>
  );
};

export default Index;
