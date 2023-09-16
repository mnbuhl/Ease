import { Head } from '@inertiajs/react';
import AuthLayout from '../../layouts/AuthLayout';
import { PageProps } from '../../page-props';
import UpdateProfileInformationForm from './Partials/UpdateProfileInformation';
import UpdatePasswordForm from './Partials/UpdatePasswordForm';
import DeleteUserForm from './Partials/DeleteUserForm';

type Props = {
  mustVerifyEmail: boolean;
  status?: string;
};

const Edit = ({ mustVerifyEmail, status }: PageProps<Props>) => {
  return (
    <>
      <Head title="Profile" />
      <div className="py-12">
        <div className="max-w-7xl mx-auto sm:px-6 lg:px-8 space-y-6">
          <div className="p-4 sm:p-8 bg-white dark:bg-gray-800 shadow sm:rounded-lg">
            <UpdateProfileInformationForm
              mustVerifyEmail={mustVerifyEmail}
              status={status}
              className="max-w-xl"
            />
          </div>

          <div className="p-4 sm:p-8 bg-white dark:bg-gray-800 shadow sm:rounded-lg">
            <UpdatePasswordForm className="max-w-xl" />
          </div>

          <div className="p-4 sm:p-8 bg-white dark:bg-gray-800 shadow sm:rounded-lg">
            <DeleteUserForm className="max-w-xl" />
          </div>
        </div>
      </div>
    </>
  );
};

Edit.layout = (page: JSX.Element) => (
  <AuthLayout
    header={
      <h2 className="font-semibold text-xl text-gray-800 dark:text-gray-200 leading-tight">
        Profile
      </h2>
    }
  >
    {page}
  </AuthLayout>
);

export default Edit;
