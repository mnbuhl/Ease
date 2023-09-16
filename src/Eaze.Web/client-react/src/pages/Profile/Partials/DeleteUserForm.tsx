import { useForm } from '@inertiajs/react';
import { FormEventHandler, useRef, useState } from 'react';
import Button from '../../../components/Button';
import Modal from '../../../components/Modal';
import Label from '../../../components/forms/Label';
import Input from '../../../components/forms/Input';
import Error from '../../../components/forms/Error';

type Props = {
  className?: string;
};

const DeleteUserForm = ({ className }: Props) => {
  const [confirmingUserDeletion, setConfirmingUserDeletion] = useState(false);
  const passwordInput = useRef<HTMLInputElement>();

  const {
    data,
    setData,
    delete: destroy,
    processing,
    reset,
    errors,
  } = useForm({
    password: '',
  });

  const confirmUserDeletion = () => {
    setConfirmingUserDeletion(true);
  };

  const deleteUser: FormEventHandler = (e) => {
    e.preventDefault();

    destroy('/profile/delete', {
      preserveScroll: true,
      onSuccess: () => closeModal(),
      onError: () => passwordInput.current?.focus(),
      onFinish: () => reset(),
    });
  };

  const closeModal = () => {
    setConfirmingUserDeletion(false);

    reset();
  };

  return (
    <section className={`space-y-6 ${className}`}>
      <header>
        <h2 className="text-lg font-medium text-gray-900 dark:text-gray-100">Delete Account</h2>

        <p className="mt-1 text-sm text-gray-600 dark:text-gray-400">
          Once your account is deleted, all of its resources and data will be permanently deleted.
          Before deleting your account, please download any data or information that you wish to
          retain.
        </p>
      </header>

      <Button variant="danger" onClick={confirmUserDeletion}>
        Delete Account
      </Button>

      <Modal show={confirmingUserDeletion} onClose={closeModal}>
        <form onSubmit={deleteUser} className="p-6">
          <h2 className="text-lg font-medium text-gray-900 dark:text-gray-100">
            Are you sure you want to delete your account?
          </h2>

          <p className="mt-1 text-sm text-gray-600 dark:text-gray-400">
            Once your account is deleted, all of its resources and data will be permanently deleted.
            Please enter your password to confirm you would like to permanently delete your account.
          </p>

          <div className="mt-6">
            <Label htmlFor="password" value="Password" className="sr-only" />

            <Input
              id="password"
              type="password"
              name="password"
              ref={passwordInput}
              value={data.password}
              onChange={(e) => setData('password', e.target.value)}
              className="mt-1 block w-3/4"
              isFocused
              placeholder="Password"
            />

            <Error message={errors.password} className="mt-2" />
          </div>

          <div className="mt-6 flex justify-end">
            <Button variant="secondary" onClick={closeModal}>
              Cancel
            </Button>

            <Button variant="danger" className="ml-3" disabled={processing}>
              Delete Account
            </Button>
          </div>
        </form>
      </Modal>
    </section>
  );
};

export default DeleteUserForm;
