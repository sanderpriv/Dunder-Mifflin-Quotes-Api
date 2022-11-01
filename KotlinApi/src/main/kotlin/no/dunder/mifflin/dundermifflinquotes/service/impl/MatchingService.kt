package no.dunder.mifflin.dundermifflinquotes.service.impl

import no.dunder.mifflin.dundermifflinquotes.domain.dto.Quote
import no.dunder.mifflin.dundermifflinquotes.service.IMatchingService
import org.springframework.stereotype.Service

@Service
class MatchingService: IMatchingService {
    override fun getMatchesOfRedditCommentsAndQuotes(originalComments: List<String>, originalQuotes: List<Quote>
    ): Map<String, Int> {

        val comments = filter(originalComments)
        val quotes = filter(originalQuotes.map { it.lineText })

        val matchesWithId = HashMap<String, Int>()

        for (quote in quotes) {
            if (!comments.contains(quote))
                continue

            matchesWithId.putIfAbsent(quote, 0)
            matchesWithId[quote] = matchesWithId[quote]!! + 1
        }

        return matchesWithId
    }

    private fun filter(strings: List<String>): List<String> {
        return strings.asSequence().toLowerCase().removePunctuations().toList()
    }

    private fun Sequence<String>.toLowerCase(): Sequence<String> {
        return this.map { it.lowercase() }
    }

    private fun Sequence<String>.removePunctuations(): Sequence<String> {
        return this.map {
            it
                .replace(",", "")
                .replace(".", "")
                .replace("!", "")
                .replace("?", "")
                .replace("'", "")
        }
    }
}