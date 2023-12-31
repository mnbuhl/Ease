import { Head } from '@inertiajs/react';
import AuthLayout from '../../layouts/AuthLayout';
import { useQuery } from '../../hooks/useApi';
import Button from '../../components/Button';

type Props = {
  status?: string;
};

const Dashboard = ({ status }: Props) => {
  const { data, loading, fetch } = useQuery<{ temp: number; summary: string }>('/weather');

  return (
    <>
      <Head title="Dashboard" />

      <div className="py-12 space-y-4">
        {status && (
          <div className="max-w-7xl mx-auto sm:px-6 lg:px-8">
            <div
              className="bg-green-100 border border-green-400 text-green-700 px-4 py-3 rounded-lg relative mb-2"
              role="alert"
            >
              {status === 'email-confirmed' && 'Your email has been confirmed!'}
            </div>
          </div>
        )}

        <div className="max-w-7xl mx-auto sm:px-6 lg:px-8">
          <div className="bg-white dark:bg-gray-800 overflow-hidden shadow-sm sm:rounded-lg">
            <div className="p-6 text-gray-900 dark:text-gray-100">You're logged in!</div>
          </div>
        </div>

        <div className="max-w-7xl mx-auto sm:px-6 lg:px-8">
          <div className="bg-white dark:bg-gray-800 overflow-hidden shadow-sm sm:rounded-lg p-6 space-y-2">
            <h3 className="text-gray-900 dark:text-gray-100 font-semibold text-lg">
              Todays weather
            </h3>
            {!loading ? (
              <>
                <div className="text-gray-900 dark:text-gray-100">Summary: {data?.summary}</div>
                <div className="text-gray-900 dark:text-gray-100">Tempeature: {data?.temp} C</div>
              </>
            ) : (
              <div className="text-gray-900 dark:text-gray-100">Loading...</div>
            )}

            <Button onClick={async () => await fetch()}>Update</Button>
          </div>
        </div>
      </div>
    </>
  );
};

Dashboard.layout = (page: JSX.Element) => (
  <AuthLayout
    header={
      <h2 className="font-semibold text-xl text-gray-800 dark:text-gray-200 leading-tight">
        Dashboard
      </h2>
    }
  >
    {page}
  </AuthLayout>
);

export default Dashboard;
