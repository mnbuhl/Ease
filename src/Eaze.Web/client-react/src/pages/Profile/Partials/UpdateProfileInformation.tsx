import { Link, useForm, usePage } from '@inertiajs/react';
import { FormEventHandler } from 'react';
import { PageProps } from '../../../page-props';
import Label from '../../../components/forms/Label';
import Input from '../../../components/forms/Input';
import Error from '../../../components/forms/Error';
import { Transition } from '@headlessui/react';
import Button from '../../../components/Button';

type Props = {
  mustVerifyEmail: boolean;
  status?: string;
  className?: string;
};

const UpdateProfileInformationForm = ({ mustVerifyEmail, status, className }: Props) => {
  const user = usePage<PageProps>().props.auth.user;

  const { data, setData, patch, errors, processing, recentlySuccessful } = useForm({
    name: user.name,
    email: user.email,
  });

  const submit: FormEventHandler = (e) => {
    e.preventDefault();

    patch('/profile/update');
  };

  return (
    <section className={className}>
      <header>
        <h2 className="text-lg font-medium text-gray-900 dark:text-gray-100">
          Profile Information
        </h2>

        <p className="mt-1 text-sm text-gray-600 dark:text-gray-400">
          Update your account's profile information and email address.
        </p>
      </header>

      <form onSubmit={submit} className="mt-6 space-y-6">
        <div>
          <Label htmlFor="name" value="Name" />

          <Input
            id="name"
            className="mt-1 block w-full"
            value={data.name}
            onChange={(e) => setData('name', e.target.value)}
            required
            isFocused
            autoComplete="name"
          />

          <Error className="mt-2" message={errors.name} />
        </div>

        <div>
          <Label htmlFor="email" value="Email" />

          <Input
            id="email"
            type="email"
            className="mt-1 block w-full"
            value={data.email}
            onChange={(e) => setData('email', e.target.value)}
            required
            autoComplete="username"
          />

          <Error className="mt-2" message={errors.email} />
        </div>

        {mustVerifyEmail && !user.emailVerified && (
          <div>
            <p className="text-sm mt-2 text-gray-800 dark:text-gray-200">
              Your email address is unverified.
              <Link
                href={'/auth/verification-email'}
                method="post"
                as="button"
                className="underline text-sm text-gray-600 dark:text-gray-400 hover:text-gray-900 dark:hover:text-gray-100 rounded-md focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500 dark:focus:ring-offset-gray-800"
              >
                Click here to re-send the verification email.
              </Link>
            </p>

            {status === 'verification-link-sent' && (
              <div className="mt-2 font-medium text-sm text-green-600 dark:text-green-400">
                A new verification link has been sent to your email address.
              </div>
            )}
          </div>
        )}

        <div className="flex items-center gap-4">
          <Button variant="primary" disabled={processing}>
            Save
          </Button>

          <Transition
            show={recentlySuccessful}
            enter="transition ease-in-out"
            enterFrom="opacity-0"
            leave="transition ease-in-out"
            leaveTo="opacity-0"
          >
            <p className="text-sm text-gray-600 dark:text-gray-400">Saved.</p>
          </Transition>
        </div>
      </form>
    </section>
  );
};

export default UpdateProfileInformationForm;
