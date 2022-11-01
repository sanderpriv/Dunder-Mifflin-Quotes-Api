package no.dunder.mifflin.dundermifflinquotes.service

import no.dunder.mifflin.dundermifflinquotes.domain.dto.Quote

interface IQuotesService {

    fun getQuotes(size: Int): List<Quote>

    fun getAllQuotes(): List<Quote>

    fun getRandomQuote(): Quote?
}