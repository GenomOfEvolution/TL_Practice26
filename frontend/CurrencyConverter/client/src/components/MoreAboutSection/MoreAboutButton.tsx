import styles from './MoreAboutButton.module.scss';

type MoreAboutButtonProps = {
  fromValue: string;
  toValue: string;
  arrowUp?: boolean;
  testId?: string;
};

export const MoreAboutButton = ({ fromValue, toValue, arrowUp = true, testId }: MoreAboutButtonProps) => {
  return (
    <button className={styles.button} data-testid={testId}>
      {fromValue}/{toValue}: about {arrowUp ? '↑' : '↓'}
    </button>
  );
};
