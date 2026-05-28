import { render, screen } from '@testing-library/react'
import MoreAboutSection from './MoreAboutSection'

describe('MoreAboutSection', () => {
  it('renders button with fromValue/toValue text and arrow', () => {
    render(<MoreAboutSection fromValue="PLN" toValue="JPY" />)

    const button = screen.getByRole('button', { name: /PLN\/JPY: about ↑/ })
    expect(button).toBeInTheDocument()
  })

  it('renders both CurrencyInfo components with testIds', () => {
    render(<MoreAboutSection fromValue="PLN" toValue="JPY" />)

    expect(screen.getByTestId('first-info')).toBeInTheDocument()
    expect(screen.getByTestId('second-info')).toBeInTheDocument()
  })
})
