package no.dunder.mifflin.dundermifflinquotes.db.impl

import org.jetbrains.exposed.sql.Table

object QuotesTable : Table() {
    val quoteId = integer("id")
    val season = integer("season")
    val episode= integer("episode")
    val scene = integer("scene")
    val lineText = text("lineText")
    val speaker = varchar("speaker", 255)
    val deleted = bool("deleted")

    override val primaryKey = PrimaryKey(quoteId)
}