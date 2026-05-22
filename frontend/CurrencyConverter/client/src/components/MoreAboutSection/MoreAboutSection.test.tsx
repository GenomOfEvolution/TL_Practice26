import { render, screen } from '@testing-library/react'
import MoreAboutSection from './MoreAboutSection'

describe('MoreAboutSection', () => {
  it('renders button with fromValue/toValue text and arrow', () => {
    render(<MoreAboutSection fromValue="PLN" toValue="JPY" />)

    const button = screen.getByRole('button', { name: /PLN\/JPY: about ↑/ })
    expect(button).toBeInTheDocument()
  })

  it('renders first CurrencyInfo with Polish zloty title', () => {
    render(<MoreAboutSection fromValue="PLN" toValue="JPY" />)

    expect(screen.getByText('Polish zloty - PLN - zł')).toBeInTheDocument()
    expect(screen.getByText(/official currency and legal tender of Poland/)).toBeInTheDocument()
  })

  it('renders second CurrencyInfo with Japanese yen title', () => {
    render(<MoreAboutSection fromValue="PLN" toValue="JPY" />)

    expect(screen.getByText('Japanese yen - JPY - ¥')).toBeInTheDocument()
    expect(screen.getByText(/official currency of Japan/)).toBeInTheDocument()
  })
})
