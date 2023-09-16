import { useForm } from '@inertiajs/react';
import { FormEventHandler, useRef } from 'react';
import Label from '../../../components/forms/Label';
import Input from '../../../components/forms/Input';
import Error from '../../../components/forms/Error';
import { Transition } from '@headlessui/react';
import Button from '../../../components/Button';

type Props = {
  className?: string;
};

const UpdatePasswordForm = ({ className }: Props) => {
  const passwordInput = useRef<HTMLInputElement>();
  const currentPasswordInput = useRef<HTMLInputElement>();

  const { data, setData, errors, put, reset, processing, recentlySuccessful } = useForm({
    currentPassword: '',
    password: '',
    passwordConfirmation: '',
  });

  const updatePassword: FormEventHandler = (e) => {
    e.preventDefault();

    put('/auth/password', {
      preserveScroll: true,
      onSuccess: () => reset(),
      onError: (errors) => {
        if (errors.password) {
          reset('password', 'passwordConfirmation');
          passwordInput.current?.focus();
        }

        if (errors.currentPassword) {
          reset('currentPassword');
          currentPasswordInput.current?.focus();
        }
      },
    });
  };

  return (
    <section className={className}>
      <header>
        <h2 className="text-lg font-medium text-gray-900 dark:text-gray-100">Update Password</h2>

        <p className="mt-1 text-sm text-gray-600 dark:text-gray-400">
          Ensure your account is using a long, random password to stay secure.
        </p>
      </header>

      <form onSubmit={updatePassword} className="mt-6 space-y-6">
        <div>
          <Label htmlFor="currentPassword" value="Current Password" />

          <Input
            id="currentPassword"
            ref={currentPasswordInput}
            value={data.currentPassword}
            onChange={(e) => setData('currentPassword', e.target.value)}
            type="password"
            className="mt-1 block w-full"
            autoComplete="current-password"
          />

          <Error message={errors.currentPassword} className="mt-2" />
        </div>

        <div>
          <Label htmlFor="password" value="New Password" />

          <Input
            id="password"
            ref={passwordInput}
            value={data.password}
            onChange={(e) => setData('password', e.target.value)}
            type="password"
            className="mt-1 block w-full"
            autoComplete="new-password"
          />

          <Error message={errors.password} className="mt-2" />
        </div>

        <div>
          <Label htmlFor="passwordConfirmation" value="Confirm Password" />

          <Input
            id="passwordConfirmation"
            value={data.passwordConfirmation}
            onChange={(e) => setData('passwordConfirmation', e.target.value)}
            type="password"
            className="mt-1 block w-full"
            autoComplete="new-password"
          />

          <Error message={errors.passwordConfirmation} className="mt-2" />
        </div>

        <div className="flex items-center gap-4">
          <Button variant="primary" loading={processing}>
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

export default UpdatePasswordForm;
