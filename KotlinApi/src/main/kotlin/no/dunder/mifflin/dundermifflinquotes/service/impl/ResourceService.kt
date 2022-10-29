package no.dunder.mifflin.dundermifflinquotes.service.impl

import no.dunder.mifflin.dundermifflinquotes.db.IResourceService
import no.dunder.mifflin.dundermifflinquotes.domain.dto.Quote
import org.apache.commons.csv.CSVFormat
import org.apache.commons.csv.CSVParser
import org.apache.commons.csv.CSVRecord
import org.springframework.core.io.Resource
import org.springframework.core.io.ResourceLoader
import org.springframework.stereotype.Service
import java.io.FileReader
import java.io.Reader


@Service
class ResourceService(private val resourceLoader: ResourceLoader) : IResourceService {

    override fun getQuotesFromCsvFile(): List<Quote> {
        val quotes = ArrayList<Quote>()

        for (record in csvRecords()) {
            val id = record.get("id").toInt()
            val season = record.get("season").toInt()
            val episode = record.get("episode").toInt()
            val scene = record.get("scene").toInt()
            val lineText = record.get("line_text")
            val speaker = record.get("speaker")
            val deleted = record.get("deleted").toBoolean()

            quotes.add(Quote(id, season, episode, scene, lineText, speaker, deleted))
        }

        return quotes
    }

    private fun csvRecords(): List<CSVRecord> {
        val classPath = String.format("classpath:%s", originalSetFilename)
        val resource: Resource = resourceLoader.getResource(classPath)
        val reader: Reader = FileReader(resource.file)

        val format = CSVFormat.Builder.create(CSVFormat.DEFAULT)
            .setHeader()
            .setSkipHeaderRecord(true)
            .build()
        val parser = CSVParser.parse(reader, format)

        return parser.records
    }

    companion object {
        const val originalSetFilename = "the-office-lines.csv"
        const val sqLiteDbSetFilename = "quotes.db"
    }
}